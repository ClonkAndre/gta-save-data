﻿using GTASaveData.Types;
using System;
using System.Diagnostics;
using System.Linq;

namespace GTASaveData.GTA3
{
    public class PathFind : SaveDataObject, IEquatable<PathFind>
    {
        private Array<PathNode> m_pathNodes;

        public Array<PathNode> PathNodes
        {
            get { return m_pathNodes; }
            set { m_pathNodes = value; OnPropertyChanged(); }
        }

        public PathFind()
        {
            m_pathNodes = new Array<PathNode>();
        }

        public PathFind(int saveSize)
        {
            m_pathNodes = CreateArray<PathNode>((saveSize / 2) * 8);
        }

        protected override void ReadObjectData(DataBuffer buf, SaveFileFormat fmt)
        {
            int size = SizeOf(this);
            byte[] data = buf.ReadBytes(size);
            int n = size / 2;

            for (int i = 0; i < PathNodes.Count; i++)
            {
                PathNodes[i].Disabled = ((data[i / 8] & (1 << i % 8)) != 0);
            }

            for (int i = 0; i < PathNodes.Count; i++)
            {
                PathNodes[i].BetweenLevels = ((data[i / 8 + n] & (1 << i % 8)) != 0);
            }

            Debug.Assert(buf.Offset == size);
        }

        protected override void WriteObjectData(DataBuffer buf, SaveFileFormat fmt)
        {
            int size = SizeOf(this);
            byte[] data = new byte[size];
            int n = size / 2;

            for (int i = 0; i < PathNodes.Count; i++)
            {
                if (PathNodes[i].Disabled)
                    data[i / 8] |= (byte) (1 << i % 8);
                else
                    data[i / 8] &= (byte) ~(1 << i % 8);
            }

            for (int i = 0; i < PathNodes.Count; i++)
            {
                if (PathNodes[i].BetweenLevels)
                    data[i / 8 + n] |= (byte) (1 << i % 8);
                else
                    data[i / 8 + n] &= (byte) ~(1 << i % 8);
            }

            buf.Write(data);
            Debug.Assert(buf.Offset == size);
        }

        protected override int GetSize(SaveFileFormat fmt)
        {
            return ((PathNodes.Count + 8 - 1) / 8) * 2;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PathFind);
        }

        public bool Equals(PathFind other)
        {
            if (other == null)
            {
                return false;
            }

            return PathNodes.SequenceEqual(other.PathNodes);
        }
    }
}
