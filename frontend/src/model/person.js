export class Person {

  constructor() {
    this.isClub = true;
    this.clubMember = null;
    this.guest = {
      firstName: null,
      lastName: null,
      address: {
        street: null,
        city: null,
        postalCode: null,
        country: null
      }
    }
  }

  getPerson(){
    if (this.isClub) {
      return this.clubMember;
    } else {
      return this.guest;
    }
  }

}
