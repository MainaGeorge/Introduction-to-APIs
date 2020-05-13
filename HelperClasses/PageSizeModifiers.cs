using Microsoft.CodeAnalysis;

namespace HPlusSport.API.HelperClasses
{
    public class PageSizeModifiers
    {

        private int _maxSizePerPage = 15;

        public int PageNumber { get; set; }

        public int Size
        {
            get => _maxSizePerPage;

            set
            {
                if (value > 0 && value < _maxSizePerPage)
                {
                    _maxSizePerPage = value;
                }
            }
        }

    }
}
