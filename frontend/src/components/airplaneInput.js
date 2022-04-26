export class AirplaneInput {

  activate(model){
    this.airplane = model.airplane;

    if(model.allowNone) {
      this.airplane.allowEmpty();
    }
  }


}
