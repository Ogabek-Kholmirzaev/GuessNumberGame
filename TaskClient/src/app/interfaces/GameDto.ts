export interface GameDto{
  id: number,
  secretNumber: number,
  numberOfTries: number,
  maximumTries: number,
  isFinished: boolean,
  isWinner: boolean,
  userId: number
}
