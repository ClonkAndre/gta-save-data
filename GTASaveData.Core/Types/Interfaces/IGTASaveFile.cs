﻿using System;
using System.Collections.Generic;

namespace GTASaveData.Types.Interfaces
{
    public interface IGTASaveFile
    {
        string Name { get; set; }
        DateTime TimeLastSaved { get; set; }
        SaveFileFormat FileFormat { get; set; }
        IReadOnlyList<SaveDataObject> Blocks { get; }
    }
}
