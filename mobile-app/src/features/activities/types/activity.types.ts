export type ActivityStatus = 'planned' | 'in_progress' | 'completed' | 'cancelled';

export type Activity = {
  id: string;
  title: string;
  status: ActivityStatus;
  plannedDate: string;
  plannedAmount?: number;
};
