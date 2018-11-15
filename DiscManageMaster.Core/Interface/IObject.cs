using System;

namespace DiscManageMaster.Core.Interface
{
    [Serializable]
    public abstract class IObject
    {
        private DateTime thisCreatedTime;
        private int thisIconIndex;

        /// <summary>
        /// The name of this object.
        /// </summary>
        private string thisName;

        /// <summary>
        /// The size of this object.
        /// </summary>
        private long thisSize;

        private bool thisHidden;

        private bool thisSpecialItem;

        public string Name
        {
            get { return thisName; }
            set { thisName = value; }
        }

        public long Size
        {
            get { return thisSize; }
            set { thisSize = value; }
        }

        public bool Hidden
        {
            get { return thisHidden; }
            set { thisHidden = value; }
        }

        public bool SpecialItem
        {
            get { return thisSpecialItem; }
            set { thisSpecialItem = value; }
        }

        public DateTime CreatedTime
        {
            get { return thisCreatedTime; }
            set { thisCreatedTime = value; }
        }

        public int IconIndex
        {
            get { return thisIconIndex; }
            set { thisIconIndex = value; }
        }
    }
}