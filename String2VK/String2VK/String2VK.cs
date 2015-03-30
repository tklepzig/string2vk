using System;

namespace String2VK
{
    class String2VK
    {
        public void SendText(string text)
        {
            IntPtr keyboardLayout = Win32.GetKeyboardLayout(0);

            while (!string.IsNullOrEmpty(text))
            {
                var vKey = Win32.VkKeyScanEx(text[0], keyboardLayout);

                var highByte = (byte)(vKey >> 8);
                var lowByte = (byte)(vKey & 0xFF);

                if ((highByte == 1))
                    SendKey(Win32.VirtualKeys.Shift, KeyState.Down);
                else if ((highByte == 2))
                    SendKey(Win32.VirtualKeys.Control, KeyState.Down);
                else if ((highByte == 4))
                    SendKey(Win32.VirtualKeys.LeftMenu, KeyState.Down);
                else if ((highByte == 6))
                    SendKey(Win32.VirtualKeys.RightMenu, KeyState.Down);

                SendKey(lowByte, KeyState.Down);
                SendKey(lowByte, KeyState.Up);

                if ((highByte == 1))
                    SendKey(Win32.VirtualKeys.Shift, KeyState.Up);
                else if ((highByte == 2))
                    SendKey(Win32.VirtualKeys.Control, KeyState.Up);
                else if ((highByte == 4))
                    SendKey(Win32.VirtualKeys.LeftMenu, KeyState.Up);
                else if ((highByte == 6))
                    SendKey(Win32.VirtualKeys.RightMenu, KeyState.Up);


                if (text.Length > 0)
                    text = text.Remove(0, 1);
                else
                    text = string.Empty;
            }
        }

        private void SendKey(Win32.VirtualKeys key, KeyState keyState, bool extended = false)
        {
            SendKey((byte)key, keyState, extended);
        }

        private void SendKey(byte key, KeyState keyState, bool extended = false)
        {
            uint flags = 0;

            if (keyState == KeyState.Up)
                flags = Win32.KEYEVENTF_KEYUP;

            if (extended)
                flags |= 1;

            Win32.keybd_event(key, Win32.MapVirtualKey(key, 0), flags, UIntPtr.Zero);
        }
    }
}