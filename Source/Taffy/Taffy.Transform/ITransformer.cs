namespace Taffy.Transform {
    public interface ITransformer {
        void ToWav(string mp3FileName, string wavFileName);
        void ToMp3(string wavFileName, string mp3FileName);
        void Stretch(string wavFileName, string stretchedWavFileName);
    }
}