using Microsoft.CodeAnalysis;

namespace HPlusSport.API.HelperClasses
{
    public class PageModifier
    {

        private const int MaxSizePerPage = 50;

        private int _defaultSizePerPage = 15;

        public int PageNumber { get; set; }

        public string Name { get; set; }

        public int Size
        {
            get => _defaultSizePerPage;

            set
            {
                if (value >= 0 && value < MaxSizePerPage)
                {
                    _defaultSizePerPage = value;
                }
            }
        }

    }
}
