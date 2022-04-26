export class Airplane {

  constructor() {
    this.types = [AirplaneType.CLUB, AirplaneType.GUEST];
    this.type ={value: 'club'};
    console.log("Selected type: ", this.type)
    this.clubAirplane = null;
    this.guestAirplane = {
      immatriculation: "OK-",
      type: null
    }
  }

  allowEmpty(){
    this.types.push(AirplaneType.NONE)
  }

  getAirplane() {
    if (this.type.value === AirplaneType.CLUB.value) {
      return this.clubAirplane;
    } else if (this.type.value === AirplaneType.GUEST.value) {
      return this.guestAirplane;
    } else {
      return null;
    }
  }
}

export const AirplaneType = {
  CLUB: {
    value: 'club',
    label: 'Klubové'
  },
  GUEST: {
    value: 'guest',
    label: 'Soukromé'
  },
  NONE: {
    value: 'none',
    label: 'Žádné'
  }
};
