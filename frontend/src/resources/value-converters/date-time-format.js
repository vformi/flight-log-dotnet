import moment from 'moment';

export class DateTimeFormatValueConverter {
  toView(value) {
    return moment(value).format('YYYY-MM-DDTkk:mm');
  }
}
