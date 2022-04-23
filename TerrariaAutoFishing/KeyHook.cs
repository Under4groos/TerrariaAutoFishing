using System;
using System.Runtime.InteropServices;



public class KeyHook : IDisposable
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string lpFileName);
    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);
    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

    private const int WH_KEYBOARD_LL = 13;
    private const int WH_KEYDOWN = 0x0100;

    private int _key;
    /// <summary>
    /// object sender, KeyPressEventArgs e
    /// </summary>
    //public event KeyPressEventHandler KeyPressed;

    public Action<char, bool> KeyPressed;
    public bool is_down = false;
    private delegate IntPtr KeyboardHookProc(int code, IntPtr wParam, IntPtr lParam);
    private KeyboardHookProc _proc;
    private IntPtr _hHook = IntPtr.Zero;


    public KeyHook(int keyCode)
    {
        _key = keyCode;
        _proc = HookProc;
    }
    public void SetHook()
    {

        _hHook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, LoadLibrary("User32"), 0);
    }
    public void Dispose()
    {
        UnHook();
    }
    public void UnHook() { UnhookWindowsHookEx(_hHook); }
    private IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
    {
        if ((code >= 0 && wParam == (IntPtr)WH_KEYDOWN) && Marshal.ReadInt32(lParam) == _key)
        {
            if (KeyPressed != null)
            {
                is_down = !is_down;
                KeyPressed(Convert.ToChar(code), is_down);
            }
        }
        return CallNextHookEx(_hHook, code, (int)wParam, lParam);
    }
}
