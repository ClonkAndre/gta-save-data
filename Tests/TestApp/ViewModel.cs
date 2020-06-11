﻿using GTASaveData;
using GTASaveData.Extensions;
using GTASaveData.GTA3;
using GTASaveData.Helpers;
using GTASaveData.Types;
using GTASaveData.Types.Interfaces;
using GTASaveData.VC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfEssentials;
using WpfEssentials.Win32;

using IIIBlock = GTASaveData.GTA3.DataBlock;
//using IVBlock = GTASaveData.GTA4.Block;
using VCBlock = GTASaveData.VC.DataBlock;
//using SABlock = GTASaveData.SA.Block;

namespace TestApp
{
    public class ViewModel : ObservableObject
    {
        #region Events, Variables, and Properties
        public EventHandler<FileDialogEventArgs> FileDialogRequested;
        public EventHandler<MessageBoxEventArgs> MessageBoxRequested;
        public EventHandler<FileTypeListEventArgs> PopulateFileTypeList;

        private SaveData m_currentSaveFile;
        private FileFormat m_currentFileFormat;
        private Game m_selectedGame;
        private int m_selectedBlockIndex;
        //private bool m_autoDetectFileType;
        private bool m_showEntireFile;
        private string m_text;
        private string m_statusText;
        private IEnumerable<FileFormat> m_fileFormats;

        public SaveData CurrentSaveFile
        {
            get { return m_currentSaveFile; }
            set { m_currentSaveFile = value; OnPropertyChanged(); }
        }

        public FileFormat CurrentFileFormat
        {
            get { return m_currentFileFormat; }
            set { m_currentFileFormat = value; OnPropertyChanged(); }
        }

        public Game SelectedGame
        {
            get { return m_selectedGame; }
            set { m_selectedGame = value; OnPopulateFileTypeList(); OnPropertyChanged(); }
        }

        public int SelectedBlockIndex
        {
            get { return m_selectedBlockIndex; }
            set { m_selectedBlockIndex = value; OnPropertyChanged(); }
        }

        public string Text
        {
            get { return m_text; }
            set { m_text = value; OnPropertyChanged(); }
        }

        public string StatusText
        {
            get { return m_statusText; }
            set { m_statusText = value; OnPropertyChanged(); }
        }

        public string[] BlockNameForCurrentGame
        {
            get { return BlockNames[SelectedGame]; }
        }

        public IEnumerable<FileFormat> FileFormats
        {
            //get { return m_fileFormats; }
            //set { m_fileFormats = value; OnPropertyChanged(); }
            get
            {
                return SelectedGame switch
                {
                    Game.GTA3 => GTA3Save.FileFormats.GetAll(),
                    Game.VC => VCSave.FileFormats.GetAll(),
                    _ => new FileFormat[0],
                };
            }
        }

        public ViewModel()
        {
            SelectedBlockIndex = -1;
        }

        public void Initialize()
        {
            OnPopulateFileTypeList();
        }
        #endregion

        #region Commands
        public ICommand FileOpenCommand
        {
            get
            {
                return new RelayCommand
                (
                    () => RequestFileDialog(FileDialogType.OpenFileDialog)
                );
            }
        }

        public ICommand FileCloseCommand
        {
            get
            {
                return new RelayCommand
                (
                    () => CloseSaveData(),
                    () => CurrentSaveFile != null
                );
            }
        }

        public ICommand FileSaveAsCommand
        {
            get
            {
                return new RelayCommand
                (
                    () => RequestFileDialog(FileDialogType.SaveFileDialog),
                    () => CurrentSaveFile != null
                );
            }
        }

        public ICommand FileExitCommand
        {
            get
            {
                return new RelayCommand
                (
                    () => Application.Current.Shutdown()
                );
            }
        }
        #endregion

        public void OnLoadingFile()
        {
            // Do random stuff here :p
        }

        public void LoadSaveData(string path)
        {
            switch (SelectedGame)
            {
                case Game.GTA3: DoLoad<GTA3Save>(path); break;
                case Game.VC: DoLoad<VCSave>(path); break;
                //case GameType.SA: DoLoad<SanAndreasSave>(path); break;
                //case GameType.LCS: DoLoad<LibertyCityStoriesSave>(path); break;
                //case GameType.VCS: DoLoad<ViceCityStoriesSave>(path); break;
                //case GameType.IV: DoLoad<GTA4Save>(path); break;
                default: RequestMessageBoxError("Selected game not yet supported!"); return;
            }

            if (CurrentSaveFile != null)
            {
                SelectedBlockIndex = 0;
                UpdateTextBox();
                StatusText = "Loaded " + path + ".";

                OnLoadingFile();
                OnPropertyChanged(nameof(BlockNameForCurrentGame));
            }
        }

        private bool DoLoad<T>(string path) where T : GTASaveData.SaveData, new()
        {
            try
            {
                bool detected = SaveData.GetFileFormat<T>(path, out FileFormat fmt);
                if (!detected)
                {
                    RequestMessageBoxError(string.Format("Unable to detect file type!"));
                    return false;
                }
                CurrentFileFormat = fmt;
                
                CleanupOldSaveData();
                CurrentSaveFile = SaveData.Load<T>(path, CurrentFileFormat);

                return true;
            }
            catch (IOException e)
            {
                RequestMessageBoxError("Failed to open file: " + e.Message);
                return false;
            }
            catch (SerializationException e)
            {
                RequestMessageBoxError("Failed to open file: " + e.Message);
                return false;
            }
        }

        private void CleanupOldSaveData()
        {
            if (CurrentSaveFile is GTA3Save)
            {
                (CurrentSaveFile as GTA3Save).Dispose();
            }
            else if (CurrentSaveFile is VCSave)
            {
                (CurrentSaveFile as VCSave).Dispose();
            }
            //else if (CurrentSaveFile is SanAndreasSave)
            //{
            //    (CurrentSaveFile as SanAndreasSave).Dispose();
            //}
        }

        //private IEnumerable<FileFormat> GetFileTypes()
        //{
        //    switch (SelectedGame)
        //    {
        //        case Game.GTA3: return GTA3Save.FileFormats.GetAll();
        //        case Game.VC: return VCSave.FileFormats.GetAll();
        //        //case GameType.SA: return SanAndreasSave.FileFormats.GetAll();
        //    }

        //    throw new NotSupportedException("Selected game not supported!");
        //}

        public void CloseSaveData()
        {
            CleanupOldSaveData();
            CurrentSaveFile = null;
            CurrentFileFormat = FileFormat.Default;
            SelectedBlockIndex = -1;

            UpdateTextBox();
            StatusText = "File closed.";
        }

        public void StoreSaveData(string path)
        {
            if (CurrentSaveFile == null)
            {
                RequestMessageBoxError("Please open a file first.");
                return;
            }

            CurrentSaveFile.FileFormat = CurrentFileFormat;
            CurrentSaveFile.TimeStamp = DateTime.Now;
            CurrentSaveFile.Save(path);
            StatusText = "File saved.";
        }

        public void UpdateTextBox()
        {
            if (CurrentSaveFile == null || SelectedBlockIndex < 0)
            {
                Text = "";
                return;
            }

            IReadOnlyList<ISaveDataObject> blocks = (CurrentSaveFile as ISaveData).Blocks;
            Text = (blocks[SelectedBlockIndex] as SaveDataObject).ToJsonString();
        }

        public void SetFileTypeByName(string fileTypeName)
        {
            CurrentFileFormat = FileFormats.FirstOrDefault(x => x.Name == fileTypeName);
        }

        public static Dictionary<Game, string[]> BlockNames => new Dictionary<Game, string[]>()
        {
            { Game.GTA3, Enum.GetNames(typeof(IIIBlock)) },
            { Game.VC, Enum.GetNames(typeof(VCBlock)) },
            //{ GameType.SA, Enum.GetNames(typeof(SABlock)) },
            //{ GameType.IV, Enum.GetNames(typeof(IVBlock)) },
        };

        #region View Operations
        private void RequestFileDialog(FileDialogType type)
        {
            FileDialogRequested?.Invoke(this, new FileDialogEventArgs(type, FileDialogRequested_Callback));
        }

        private void FileDialogRequested_Callback(bool? result, FileDialogEventArgs e)
        {
            if (result != true)
            {
                return;
            }

            switch (e.DialogType)
            {
                case FileDialogType.OpenFileDialog:
                    LoadSaveData(e.FileName);
                    break;
                case FileDialogType.SaveFileDialog:
                    StoreSaveData(e.FileName);
                    break;
            }
        }

        private void RequestMessageBoxError(string text)
        {
            MessageBoxRequested?.Invoke(this, new MessageBoxEventArgs(
                text, "Error", icon: MessageBoxImage.Error));
        }

        private void OnPopulateFileTypeList()
        {
            PopulateFileTypeList?.Invoke(this, new FileTypeListEventArgs(FileFormats));
        }
        #endregion
    }
}