using Monocle;

namespace Celeste.Mod.DoubleLanguages
{
    public class TranslationData: Component
    {
        public string translatedString;

        public TranslationData(string val): base(false, false )
        {
            translatedString = DoubleLanguages.Translate(val);
        }
        public TranslationData(bool active, bool visible) : base(active, visible) { }
    }
}