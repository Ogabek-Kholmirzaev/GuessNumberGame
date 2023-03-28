import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GameService } from '../game.service';
import { HttpErrorResponse } from '@angular/common/http';
import { GameDto } from '../interfaces/GameDto';
import { MatDialog } from '@angular/material/dialog';
import { StartGameComponent } from '../start-game/start-game.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {

  constructor(public dialog: MatDialog) { }

  openDialog(){
    this.dialog.open(StartGameComponent);
  }
}
