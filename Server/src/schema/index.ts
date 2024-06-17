import BaseUser         	from './Users/baseUser';
import User             	from './Users/user';
import Admin            	from './Users/admin';
import Baggage         	 	from './Transportation/baggage';
import City             	from './Etc/city';
import Country          	from './Etc/country';
import Guide            	from './Users/guide';
import Image                from './Etc/image';
import Review               from './Trips/review';
import TransportationType   from './Transportation/transportationType';
import TravelParty          from './Trips/travelParty';
import Trip                 from './Trips/trip';
import Point                from './Points/point';
import DateTime 			from './Etc/dateTime';
import PointType 			from './Points/pointType';
import TripPoint 			from './Points/tripPoint';
import TransportationRent 	from './Transportation/transportationRent';
import Price 				from './Trips/price';
import Consultant 			from './Users/consultant';

export {
	BaseUser,
	User,
	Admin,
	Consultant,
	Guide,
	Baggage,
	City,
	Country,
	Image,
	Review,
	TransportationType,
	TravelParty,
	Trip,
	DateTime,
	Point,
	PointType,
	TripPoint,
	TransportationRent,
	Price,
};

export default [
	BaseUser,
	User,
	Admin,
	Consultant,
	Guide,
	Baggage,
	City,
	Country,
	Image,
	Review,
	TransportationType,
	TravelParty,
	Trip,
	DateTime,
	Point,
	PointType,
	TripPoint,
	TransportationRent,
	Price,
]