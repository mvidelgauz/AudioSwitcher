﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using AudioSwitcher.AudioApi.CoreAudio.Threading;
using AudioSwitcher.AudioApi.Session;

namespace AudioSwitcher.AudioApi.CoreAudio
{
    internal class CoreAudioSession : IAudioSession
    {
        private readonly IAudioSessionControl2 _control;
        private readonly ISimpleAudioVolume _volume;

        public string SessionId
        {
            get
            {
                return ComThread.Invoke(() =>
                {
                    string id;
                    _control.GetSessionIdentifier(out id);
                    return id;
                });
            }
        }

        public uint ProcessId
        {
            get
            {
                return ComThread.Invoke(() =>
                {
                    uint processId;
                    _control.GetProcessId(out processId);
                    return processId;
                });
            }
        }

        public string DisplayName
        {
            get
            {
                return ComThread.Invoke(() =>
                {
                    string display;
                    _control.GetDisplayName(out display);
                    return display;
                });
            }
        }

        public bool IsSystemSession
        {
            get
            {
                return ComThread.Invoke(() =>
                {
                    return _control.IsSystemSoundsSession() == 0;
                });
            }
        }

        public int Volume
        {
            get
            {
                return ComThread.Invoke(() =>
                {
                    float vol;
                    _volume.GetMasterVolume(out vol);
                    return (int)vol * 100;
                });
            }
            set
            {
                ComThread.Invoke(() =>
                {
                    _volume.SetMasterVolume(((float)value) / 100, Guid.Empty);
                });
            }
        }

        public AudioSessionState SessionState
        {
            get
            {
                return ComThread.Invoke(() =>
                {
                    EAudioSessionState state;
                    _control.GetState(out state);
                    return (AudioSessionState)state;
                });
            }
        }

        public CoreAudioSession(IAudioSessionControl control)
        {
            ComThread.Assert();

            _control = control as IAudioSessionControl2;
            _volume = control as ISimpleAudioVolume;
        }
    }
}
