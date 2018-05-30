using System;

namespace Legends.ORM.Attributes
{
    public class TranslationAttribute : Attribute
    {
        public bool readingMode;

        public TranslationAttribute(bool readingMode = true)
        {
            this.readingMode = readingMode;
        }
    }
}
