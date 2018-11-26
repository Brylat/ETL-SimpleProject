import { environment as env } from '@env/environment';

export interface Brand {
    value: string;
    viewValue: string;
}

export const brands: Brand[] = [
    { value: 'audi', viewValue: 'audi' },
    { value: 'bentley', viewValue: 'bentley' },
    { value: 'bmw', viewValue: 'bmw' },
    { value: 'cadillac', viewValue: 'cadillac' },
    { value: 'chevrolet', viewValue: 'chevrolet' },
    { value: 'chrysler', viewValue: 'chrysler' },
    { value: 'citroen', viewValue: 'citroen' },
    { value: 'dacia', viewValue: 'dacia' },
    { value: 'daewoo', viewValue: 'daewoo' },
    { value: 'daihatsu', viewValue: 'daihatsu' },
    { value: 'dodge', viewValue: 'dodge' },
    { value: 'ferrari', viewValue: 'ferrari' },
    { value: 'fiat', viewValue: 'fiat' },
    { value: 'ford', viewValue: 'ford' },
    { value: 'gmc', viewValue: 'gmc' },
    { value: 'honda', viewValue: 'honda' },
    { value: 'hummer', viewValue: 'hummer' },
    { value: 'hyundai', viewValue: 'hyundai' },
    { value: 'infiniti', viewValue: 'infiniti' },
    { value: 'jaguar', viewValue: 'jaguar' },
    { value: 'jeep', viewValue: 'jeep' },
    { value: 'kia', viewValue: 'kia' },
    { value: 'lamborghini', viewValue: 'lamborghini' },
    { value: 'lancia', viewValue: 'lancia' },
    { value: 'lexus', viewValue: 'lexus' },
    { value: 'maserati', viewValue: 'maserati' },
    { value: 'mazda', viewValue: 'mazda' },
    { value: 'mclaren', viewValue: 'mclaren' },
    { value: 'mini', viewValue: 'mini' },
    { value: 'mitsubishi', viewValue: 'mitsubishi' },
  ];

export interface Price {
    value: string;
    viewValue: string;
}

export const prices: Price[] = [
    { value: '1000', viewValue: '1000 PLN' },
    { value: '2500', viewValue: '2500 PLN' },
    { value: '5000', viewValue: '5000 PLN' },
    { value: '10000', viewValue: '10 000 PLN' },
    { value: '15000', viewValue: '15 000 PLN' },
    { value: '20000', viewValue: '20 000 PLN' },
    { value: '30000', viewValue: '30 000 PLN' },
    { value: '40000', viewValue: '40 000 PLN' },
    { value: '50000', viewValue: '50 000 PLN' },
    { value: '75000', viewValue: '75 000 PLN' },
    { value: '100000', viewValue: '100 tyś PLN' },
    { value: '200000', viewValue: '200 tyś PLN' },
    { value: '300000', viewValue: '300 tyś PLN' },
    { value: '400000', viewValue: '400 tyś PLN' },
    { value: '500000', viewValue: '500 tyś PLN' },
  ];

export interface FuelType {
    value: string;
    viewValue: string;
}

export const fuelList: FuelType[] = [
    { value: 'petrol', viewValue: 'Benzyna' },
    { value: 'diesel', viewValue: 'Diesel' },
    { value: 'petrol-lpg', viewValue: 'Benzyna+LPG' },
    { value: 'petrol-cng', viewValue: 'Benzyna+CNG' },
    { value: 'electric', viewValue: 'Elektryczny' },
    { value: 'etanol', viewValue: 'Etanol' },
    { value: 'hybrid', viewValue: 'Hybryda' },
    { value: 'hidrogen', viewValue: 'Wodór' },
  ];

export interface Year{
    value: string;
    viewValue: string;
}

export const yearList: Year[] = [
    { value: '2018', viewValue: '2018' },
    { value: '2017', viewValue: '2017' },
    { value: '2016', viewValue: '2016' },
    { value: '2015', viewValue: '2015' },
    { value: '2014', viewValue: '2014' },
    { value: '2013', viewValue: '2013' },
    { value: '2012', viewValue: '2012' },
    { value: '2011', viewValue: '2011' },
    { value: '2010', viewValue: '2010' },
    { value: '2009', viewValue: '2009' },
    { value: '2008', viewValue: '2008' },
    { value: '2007', viewValue: '2007' },
    { value: '2006', viewValue: '2006' },
    { value: '2005', viewValue: '2005' },
    { value: '2004', viewValue: '2004' },
    { value: '2003', viewValue: '2003' },
    { value: '2002', viewValue: '2002' },
    { value: '2001', viewValue: '2001' },
    { value: '2000', viewValue: '2000' },
    { value: '1999', viewValue: '1999' },
    { value: '1998', viewValue: '1998' },
    { value: '1997', viewValue: '1997' },
    { value: '1996', viewValue: '1996' },
    { value: '1995', viewValue: '1995' },
    { value: '1994', viewValue: '1994' },
    { value: '1993', viewValue: '1993' },
    { value: '1992', viewValue: '1992' },
    { value: '1991', viewValue: '1991' },
    { value: '1990', viewValue: '1990' },
  ];

export interface Distance{
    value: string;
    viewValue: string;
}

export const distList: Distance[] = [
    { value: '0', viewValue: '+ 0 km' },
    { value: '5', viewValue: '+ 5 km' },
    { value: '15', viewValue: '+ 15 km' },
    { value: '25', viewValue: '+ 25 km' },
    { value: '50', viewValue: '+ 50 km' },
    { value: '75', viewValue: '+ 75 km' },
    { value: '100', viewValue: '+ 100 km' },
    { value: '150', viewValue: '+ 150 km' },
    { value: '200', viewValue: '+ 200 km' },
    { value: '250', viewValue: '+ 250 km' },
    { value: '300', viewValue: '+ 300 km' },
  ];