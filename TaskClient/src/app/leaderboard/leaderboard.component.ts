import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { GameService } from '../game.service';
import { MatTableDataSource } from '@angular/material/table';
import { UserStats } from '../interfaces/UserStats';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent {

  displayedColumns: string[] = ['id', 'userName', 'totalWins', 'totalGames', 'totalTries', 'successRate'];
  dataSource: any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(public gameService: GameService) { }

  ngOnInit(): void {
    this.gameService.getLeaderboard().subscribe(
      (data: any) => {
        console.log('UsersStats - ', data);
        this.dataSource = new MatTableDataSource<UserStats>(data as UserStats[]);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      (error: HttpErrorResponse) => {
        alert(error.error.error);
        //this.router.navigate(['/']).then(() => { window.location.reload() });
      }
    );
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
