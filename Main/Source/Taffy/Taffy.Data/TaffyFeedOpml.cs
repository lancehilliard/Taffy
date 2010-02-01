namespace Taffy.Data {
    public class TaffyFeedOpml : ITaffyFeedOpml {
        #region ITaffyFeedOpml Members

        public string Filename { get; set; }
        public string Contents { get; set; }

        #endregion
    }

    public interface ITaffyFeedOpml {
        string Filename { get; set; }
        string Contents { get; set; }
    }
}