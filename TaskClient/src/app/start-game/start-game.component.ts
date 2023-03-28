import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GameService } from '../game.service';
import { Router } from '@angular/router';
import { GameDto } from '../interfaces/GameDto';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.css']
})
export class StartGameComponent {

  userForm!: FormGroup;
  userName: string = '';

  constructor(private gameService: GameService,
              private dialogReference: MatDialogRef<StartGameComponent>,
              private router: Router,
              private formBuilder: FormBuilder){
    this.userName = localStorage.getItem('userName') ?? '';
    this.userForm = this.formBuilder.group({userName: new FormControl(`${this.userName}`, Validators.required)});
  }

  get f(){
    return this.userForm.controls;
  }

  close(){
    this.dialogReference.close();
  }

  submit(){
    console.log(this.userForm.value);

    this.gameService.createGame(this.userForm.value).subscribe(
    (data: any) => {
      var gameDto = data as GameDto;

      console.log('new-game -- ', data);

      localStorage.setItem('userName', this.userForm.get('userName')?.value);
      localStorage.setItem('userId', gameDto.userId.toString());

      //alert('Successfully created!');

      this.dialogReference.close();
      this.router.navigate(['/game/' + gameDto.id]);
    },
    (error: HttpErrorResponse) => {
      alert(error.error.error);
      //this.router.navigate(['/']).then(() => { window.location.reload() });
    }
    );
  }
}
