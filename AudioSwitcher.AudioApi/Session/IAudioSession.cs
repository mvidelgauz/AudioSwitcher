﻿using System;

namespace AudioSwitcher.AudioApi.Session
{
    public interface IAudioSession
    {
        string SessionId { get; }

        uint ProcessId { get; }

        string DisplayName { get; }

        bool IsSystemSession { get; }

        int Volume { get; set; }

        AudioSessionState SessionState { get; }
    }
}