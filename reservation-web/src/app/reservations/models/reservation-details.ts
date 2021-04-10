export class ReservationDetails {
    public id: number;
    public day: Date;
    public start: Date;
    public end: Date;
    public tableId: number;
    public tableSeats: number;
    public tableNumber: number;
    public linkedTableDetails: ReservationDetails[];
    public userFullName: string;
    public userEmail: string;
    public userPhoneNumber: string;
}
