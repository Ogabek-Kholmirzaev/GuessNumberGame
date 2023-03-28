import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GameService } from '../game.service';
import { HttpErrorResponse } from '@angular/common/http';
import { GameDto } from '../interfaces/GameDto';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GuessResponse } from '../interfaces/GuessResponse';
import { GuessRequest } from '../interfaces/GuessRequest';
import { User } from '../interfaces/User';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  id: any;
  guessNumber!: number;
  gameDto?: GameDto;
  guessResponse?: GuessResponse;
  numberForm: FormGroup;
  otherTriesResult: string[] = [];

  constructor(private activatedRoute: ActivatedRoute,
              private gameService: GameService,
              private router: Router,
              private formBuilder: FormBuilder) {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');

    this.gameService.getGameById(this.id).subscribe(
      (data: any) => {
        this.gameDto = data as GameDto;

        console.log('get-game-by-id -- ', data);

        if (this.gameDto.userId.toString() !== localStorage.getItem('userId')){
          //alert('This is not your game!');
          this.router.navigate(['/']);
        }
      },
      (error: HttpErrorResponse) => {
        /*alert(error.error.error);
        this.router.navigate(['/']);*/
      }
    );

    this.numberForm = this.formBuilder.group({
      thousands: new FormControl('', [Validators.required, Validators.min(0), Validators.max(9)]),
      hundreds: new FormControl('', [Validators.required, Validators.min(0), Validators.max(9)]),
      tens: new FormControl('', [Validators.required, Validators.min(0), Validators.max(9)]),
      ones: new FormControl('', [Validators.required, Validators.min(0), Validators.max(9)])
    });
  }
  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');

    this.gameService.getGameById(this.id).subscribe(
      (data: any) => {
        this.gameDto = data as GameDto;

        console.log('get-game-by-id -- ', data);

        if (this.gameDto.userId.toString() !== localStorage.getItem('userId')){
          alert('This is not your game!');
          this.router.navigate(['/']);
        }
      },
      (error: HttpErrorResponse) => {
        alert(error.error.error);
        this.router.navigate(['/']);
      }
    );
  }

  submitForm(){
    var userName = localStorage.getItem('userName');
    var gameId = this.id;
    this.guessNumber = this.getNumber();

    if(userName === null)
      this.router.navigate(['/']);

    const guessRequest: GuessRequest = {
      userName: userName ?? 'smth',
      gameId: gameId,
      guessNumber: this.getNumber()
    };

    this.gameService.guessNumber(guessRequest).subscribe(
      (data: any) => {
        this.guessResponse = data as GuessResponse;
        this.otherTriesResult
          .push(`Message: ${this.guessResponse.message} | Guess Number: ${guessRequest.guessNumber}`);
      },
      (error: HttpErrorResponse) => {
        alert(error.error.error);
      }
    );

    this.gameService.getGameById(this.id).subscribe(
      (data: any) => {
        this.gameDto = data as GameDto;

        console.log('get-game-by-id -- ', data);

        if (this.gameDto.userId.toString() !== localStorage.getItem('userId')){
          alert('This is not your game!');
          this.router.navigate(['/']);
        }
      },
      (error: HttpErrorResponse) => {
        alert(error.error.error);
        this.router.navigate(['/']);
      }
    );

    this.numberForm.reset({});
  }

  get f(){
    return this.numberForm.controls;
  }

  getNumber(): number{
    const guessNumber = this.numberForm.get('thousands')?.value * 1000
                      + this.numberForm.get('hundreds')?.value * 100
                      + this.numberForm.get('tens')?.value * 10
                      + this.numberForm.get('ones')?.value;

    return guessNumber;
  }

  isZeroTries(): boolean{
    if(!this.gameDto) return false;

    if(this.gameDto?.numberOfTries === 0) return true;
    return false;
  }

  isGuessResponseNotNull(): boolean{
    if(this.guessResponse)
      return true;

    return false;
  }

  newGame(){
    if(localStorage.getItem('userName') === null)
      this.router.navigate(['/']).then(() => { window.location.reload() });

    const user: User = {
      userName: localStorage.getItem('userName') ?? 'smth'
    }

    console.log(user);

    this.gameService.createGame(user).subscribe(
      (data: any) => {
        console.log('new-game -- ', data);

        this.router.navigate(['/game/' + data.id]).then(() => { window.location.reload() });
      },
      (error: HttpErrorResponse) => {
        alert(error.error.error);
        //this.router.navigate(['/']).then(() => { window.location.reload() });
      }
    );
  }

  getUserName(){
    return localStorage.getItem('userName');
  }

  getNumberOfTries(): number{
    if(this.guessResponse)
      return this.guessResponse.numberOfTries;
    else
      return 8 - this.gameDto!.numberOfTries;
  }

  isOtherTriesResultNotNull(): boolean{
    if(this.otherTriesResult.length > 0)
      return true;

    return false;
  }
}
