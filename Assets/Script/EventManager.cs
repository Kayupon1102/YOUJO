using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityRawInput;
using System.Diagnostics;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lparam);

    public GameObject userObj;
    public GameObject ui;
    UserData user;
    List<GameObject> panels;
    public ActiveApp[] activeApps = new ActiveApp[1000];
    int appCount = 0;
    float timeVal = 0;
    //public ActiveApp[] windowedApps;


    [Serializable]
    public struct ActiveApp {
        private IntPtr hWnd;
        public IntPtr HWnd {
            set {
                _hWnd = value.ToInt32();
                hWnd = value;
            }
            get { return hWnd; }
        }
        public Process process;
        public int pID;
        public string exeName;
        public string exePath;
        public int _hWnd;
    }

    public void ToggleRawKey(bool t) {
        if (t) RawKeyInput.Start(true);
        else RawKeyInput.Stop();
    }

    void Start() {
        //Main();
        var watch = Stopwatch.StartNew();
        EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
        watch.Stop();
        UnityEngine.Debug.Log($"process time {watch.ElapsedMilliseconds} msec");
        //ModuleName();
        user = userObj.GetComponent<User>().user;
        RawKeyInput.Start(true);
        RawKeyInput.OnKeyDown += OnKeyDown;
    }

    
    private bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam) {
        int pID;
        GetWindowThreadProcessId(hWnd, out pID);
        try {
            Process process = Process.GetProcessById(pID);
            activeApps[appCount].exeName = process.MainModule.ModuleName;
            activeApps[appCount].exePath = process.MainModule.FileName;
        }
        catch  {
            //UnityEngine.Debug.LogError(e);  //なぜか見つからない場合がある
            return true;                    //見なかったことにして次へ進む
        }
        activeApps[appCount].pID = pID;
        activeApps[appCount].HWnd = hWnd;
        appCount++;
        return true;                        //trueで次のhWnd
    }
    

    void OnKeyDown(RawKey key) {
        UnityEngine.Debug.Log("Puress " + key);
    }

    void OnApplicationQuit() {
        ToggleRawKey(false);
    }

    void Update() {
        /*
        timeVal += Time.deltaTime;
        if (timeVal > 10) {
            appCount = 0;
            EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
            //ModuleName();
            timeVal = 0;
        }
        */
    }
    /*
    public void ModuleName() {
        try {
            Process[] process = Process.GetProcesses();
            if (process.Length > activeApps.Length) throw new IndexOutOfRangeException(message: "Apps too much!");
            int x = 0;
            for (int i = 0; i < process.Length; i++) {
                if (!process[i].HasExited) {
                    if (!(process[i].MainWindowHandle == IntPtr.Zero)) {
                        activeApps[x].exePath = process[i].MainModule.FileName;
                        activeApps[x].exeName = process[i].MainModule.ModuleName;
                        activeApps[x].HWnd = process[i].MainWindowHandle;
                        x++;
                    }
                }
            }
        }
        catch (Exception e) {
            UnityEngine.Debug.LogError(e.Message);
        }
    }
    */


    /*
    private static Dictionary<int, IntPtr> _DictHWnd = new Dictionary<int, IntPtr>();
    static List<ActiveApp> listApp = new List<ActiveApp>();

    static void Main() {
        var watch = Stopwatch.StartNew();
        EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
        
        var procs = Process.GetProcesses();
        foreach (var proc in procs) {
            if (_DictHWnd.ContainsKey(proc.Id)) {
                var app = new ActiveApp() { pID = proc.Id };

                try {
                    app.exePath = proc.MainModule.FileName;
                    app.exeName = proc.MainModule.ModuleName;
                    app.HWnd = _DictHWnd[proc.Id];
                    listApp.Add(app);
                }
                catch {
                }
            }
        }
        watch.Stop();
        UnityEngine.Debug.Log($"process time {watch.ElapsedMilliseconds} msec");
        //Console.WriteLine($"process time {watch.ElapsedMilliseconds} msec");
        
        foreach (var app in listApp) {
            Console.WriteLine($"pID:{app.pID} hWnd:{app.HWnd} exePath:{app.exePath.Substring(0, 4)}*****");
        }
        
        //Console.ReadKey();
    }

    private static bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam) {
        int pID;
        GetWindowThreadProcessId(hWnd, out pID);
        _DictHWnd[pID] = hWnd;
        return true;
    }
    */
}
