import { Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { EpisodesListComponent } from './episodes/episodes-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    HttpClientModule,
    EpisodesListComponent
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class AppComponent { }