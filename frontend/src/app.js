import {inject} from 'aurelia-framework';
import 'bootstrap'; // load bootstrap JavaScript
import {PLATFORM} from 'aurelia-pal';


export class App {

  constructor() {

  }

  configureRouter(config, router){
    config.title = 'Flight Log';
    config.map([
      { route: '',              moduleId: PLATFORM.moduleName('./pages/current-flights'),   title: 'Lety', nav: true},
      { route: 'newFlight',     moduleId: PLATFORM.moduleName('./pages/new-flight'),   title: 'Nový let',  nav: true },
      { route: 'report',        moduleId: PLATFORM.moduleName('./pages/report'),   title: 'Report',  nav: true }

    ]);

    this.router = router;
  }
}
