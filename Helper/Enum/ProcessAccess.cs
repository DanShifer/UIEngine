namespace UIEngine.Helper.Enum
{
    /// <summary>
    /// Доступ к процессу
    /// </summary>
    public enum ProcessAccess : uint
    {
        All = 0x001F0FFF,
        CreateThread = 0x00000002,
        DuplicateHandle = 0x00000040,
        QueryInformation = 0x00000400,
        SetInformation = 0x00000200,
        Terminate = 0x00000001,
        VMOperation = 0x00000008,
        VMRead = 0x00000010,
        VMWrite = 0x00000020,
        Synchronize = 0x00100000
    };
}