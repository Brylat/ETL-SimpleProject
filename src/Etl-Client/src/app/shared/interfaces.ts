export interface CarAdData {
  offer: string;
  category: string;
  brand: string;
  model: string;
  version: string;
  productionYear: string;
  mileage: string;
  capacity: string;
  fuel: string;
  horsePower: string;
  transmission: string;
  drivingGear: string;
  type: string;
  doorsNumber: string;
  seatsNumber: string;
  colour: string;
  isMetallic: string;
  condition: string;
  firstRegistration: string;
  isRegisteredInPoland: string;
  countryOfOrigin: string;
  isFirstOwner: string;
  noAccidents: string;
  serviceHistory: string;
  vin: string;
  particleFilter: string;
  equipment: string[];
  description: string;
}
export interface Car {
  modelName: string;
  modelValue: string;
}
export interface Model {
  value: string;
  viewValue: string;
}
export interface City {
  id: string;
  name: string;
}
export interface IUserResponse {
  results: City[];
}
export class OtomotoUrl {
  value: string;
}
export class City {
  constructor(public id: string, public name: string) {}
}
export class EtlCommand {
  constructor(public url: string) {}
}
export interface CarContainer {
  cars: Car[];
}
