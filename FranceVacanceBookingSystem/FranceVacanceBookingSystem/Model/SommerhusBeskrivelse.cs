using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranceVacanceBookingSystem.Model
{
    class SommerhusBeskrivelse
    {
        private double _distancefromwater;
        private int _bathrooms;
        private int _parkinglots;
        private int _bedrooms;
        private string _location;
        private bool _petsallowed;
        private double _price;
        private double _size;

        public double Distancefromwater
        {
            get
            {
                return _distancefromwater;
            }

            set
            {
                _distancefromwater = value;
            }
        }
        public int Bathrooms
        {
            get
            {
                return _bathrooms;
            }

            set
            {
                _bathrooms = value;
            }
        }
        public int Parkinglots
        {
            get
            {
                return _parkinglots;
            }

            set
            {
                _parkinglots = value;
            }
        }
        public int Bedrooms
        {
            get
            {
                return _bedrooms;
            }

            set
            {
                _bedrooms = value;
            }
        }
        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }
        public bool Petsallowed
        {
            get
            {
                return _petsallowed;
            }

            set
            {
                _petsallowed = value;
            }
        }
        public double Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
            }
        }
        public double Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }

        public SommerhusBeskrivelse()
        {
            
        }

        public SommerhusBeskrivelse(double distancefromwater, int bathrooms, int parkinglots, int bedrooms, string location, bool petsallowed, double price, double size)
        {
            _distancefromwater = distancefromwater;
            _bathrooms = bathrooms;
            _parkinglots = parkinglots;
            _bedrooms = bedrooms;
            _location = location;
            _petsallowed = petsallowed;
            _price = price;
            _size = size;
        }
    }


}
