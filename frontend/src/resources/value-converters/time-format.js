import moment from 'moment';

export class TimeFormatValueConverter {
  toView(value) {
    if(value == null) {
      return "--"
    }
    return moment(value).format('H:mm:ss');
  }
}
