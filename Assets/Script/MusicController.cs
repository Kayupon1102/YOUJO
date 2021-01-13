using UnityEngine;


public class MusicController : MonoBehaviour {

    private void Start() {
        
    }

    private void Update() {
        
    }

    /*
    public string[] audioDevices;
    public string mixer;
    public int bpm;
    private AudioClip audioClip;
    public int flc = 44100, minBPM = 100, MaxBPM = 240, FRAME_LEN = 525, sampleSec = 5;//flc = 44100, minBPM = 100, MaxBPM = 240, FRAME_LEN = 525
    private byte busyCount;
    private float time = 0;
    private float[] samples, frames;
    private bool musicPlaying, mixerExist;
    private string dump = "";

    float han_window(int i, int size) {
        return 0.5f - 0.5f * Mathf.Cos((float)2.0 * Mathf.PI * i / size);
    }

    void Start() {
        mixer = "";
        mixerExist = false;
        busyCount = 0;
        audioDevices = Microphone.devices;
        foreach (var device in audioDevices) {
            Debug.Log("Imput Name:" + device);
            if (device.Contains("ステレオ ミキサー")) {
                mixer = device;
                mixerExist = true;
            }
        }

        if (mixerExist) {
            audioClip = Microphone.Start(mixer, true, 1, flc);
        }
    }

    void Update() {
        time += Time.deltaTime;
        if (mixerExist) {
            if (musicPlaying && time >= sampleSec) {
                time = 0;
                Playing();

            }
            else if (!musicPlaying && time >= 1f) {
                time = 0;

                samples = new float[audioClip.samples * audioClip.channels];
                frames = new float[audioClip.samples * audioClip.channels / FRAME_LEN];
                audioClip.GetData(samples, 0);

                float volAve = 0;
                //フレームごとの音量の実行値を計算しframe[]に代入
                for (int i = 0; i < frames.Length; i++) {
                    for (int j = 0; j < FRAME_LEN; j++) {
                        frames[i] += samples[FRAME_LEN * i + j] * samples[FRAME_LEN * i + j];
                    }
                    frames[i] = Mathf.Sqrt(frames[i] / FRAME_LEN) * 1000;
                    volAve += frames[i];
                }

                volAve = volAve / frames.Length;
                if (volAve > 1) busyCount++;
                if (busyCount > 3) {
                    Debug.Log("music on");
                    busyCount = 0;
                    musicPlaying = true;
                    Microphone.End(mixer);
                    audioClip = Microphone.Start(mixer, true, sampleSec, flc);
                }
            }
        }
    }

    void Playing() {

        samples = new float[audioClip.samples * audioClip.channels];
        frames = new float[audioClip.samples * audioClip.channels / FRAME_LEN];
        audioClip.GetData(samples, 0);

        //フレームごとの音量の実行値を計算しframe[]に代入
        float volAve = 0;
        for (int i = 0; i < frames.Length; i++) {
            for (int j = 0; j < FRAME_LEN; j++) {
                frames[i] += samples[FRAME_LEN * i + j] * samples[FRAME_LEN * i + j];
            }
            frames[i] = Mathf.Sqrt(frames[i] / FRAME_LEN) * 1000;
            volAve += frames[i];
        }

        //音量の平均値が一定以下なら再生停止と判断
        volAve = volAve / frames.Length;
        if (volAve < 1) {
            SaveText("\\Dump\\" + busyCount.ToString() + ".csv", dump);
            Debug.Log("music off");
            musicPlaying = false;
            Microphone.End(mixer);
            audioClip = Microphone.Start(mixer, true, 1, flc);
            busyCount = 0;
            return;
        }


        //ひとつ前のフレームからの音量の増加量を算出
        float[] dVol = new float[frames.Length];
        dVol[1] = frames[1];
        for (int i = 0; i < frames.Length - 1; i++) {
            dVol[i + 1] = frames[i + 1] - frames[i];
            if (dVol[i + 1] < 0) dVol[i + 1] = 0;
        }

        //区間[minBPM,MaxBPM]で1刻みのBPMマッチ度を算出
        float[] aBpm = new float[MaxBPM - minBPM + 1];
        float[] bBpm = new float[MaxBPM - minBPM + 1];
        float[] rBpm = new float[MaxBPM - minBPM + 1];
        float s = flc / FRAME_LEN, bpmTmp = 0;
        for (int i = 0; i < MaxBPM - minBPM + 1; i++) {

            for (int j = 0; j < frames.Length; j++) {
                float win = han_window(j, frames.Length);
                aBpm[i] += dVol[j] * Mathf.Cos(2 * Mathf.PI * (i + minBPM) / 60 * j / s) * win;
                bBpm[i] += dVol[j] * Mathf.Sin(2 * Mathf.PI * (i + minBPM) / 60 * j / s) * win;
            }
            rBpm[i] = Mathf.Sqrt(aBpm[i] * aBpm[i] + bBpm[i] * bBpm[i]);
            dump += "," + rBpm[i].ToString();
            if (bpmTmp < rBpm[i]) {
                bpm = i + minBPM; //マッチ度が最大のものを記録
                bpmTmp = rBpm[i];
            }
        }
        dump += "\n";
        Debug.Log(bpm);
        busyCount++;
        return;
    }


    bool SaveText(string path, string text) {
        try {
            using (StreamWriter writer = new StreamWriter(Application.dataPath + path, false)) {
                writer.Write(text);
                writer.Flush();
                writer.Close();
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
            return false;
        }
        return true;
    }
    */

}
