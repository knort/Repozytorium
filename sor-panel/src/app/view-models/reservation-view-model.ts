import { TableViewModel } from './table-view-model';

export class ReservationViewModel {
        tableId: string;
        table: TableViewModel;
        userId: string;
        userViewModel: any;
        date: Date;
        start: Date;
        end: Date;
}
