import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, catchError, of, tap } from 'rxjs';
import { GuessRequest } from './interfaces/GuessRequest';
import { GameDto } from './interfaces/GameDto';
import { GuessResponse } from './interfaces/GuessResponse';
import { UserStats } from './interfaces/UserStats';

@Injectable({
  providedIn: 'root'
})
export class GameService implements CanActivate {

  baseUrl: string = 'https://localhost:7150/api/Games';

  constructor(private http: HttpClient) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    var userName = localStorage.getItem('userName');
    var userId = localStorage.getItem('userId');

    if(userId === null) return false;
    if(userName == null) return false;

    return true;
  }

  createGame(userForm: any): Observable<GameDto>{
    return this.http.post<GameDto>(this.baseUrl + '/new-game', userForm);
  }

  getGameById(gameId: number): Observable<GameDto> {
    return this.http.get<GameDto>(this.baseUrl + '/game/' + gameId);
  }

  guessNumber(guessRequest: GuessRequest): Observable<GuessResponse>{
    return this.http.post<GuessResponse>(this.baseUrl + '/guess-number', guessRequest);
  }

  getLeaderboard(): Observable<UserStats[]> {
    return this.http.get<UserStats[]>(this.baseUrl + '/leaderboard');
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      alert(`${operation} failed: ${error.error}\n${error.message}`);

      return of(result as T);
    };
  }
}
