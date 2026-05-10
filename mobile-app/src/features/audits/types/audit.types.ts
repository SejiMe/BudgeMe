export type AuditItemClassification = 'need' | 'want';

export type AuditList = {
  id: string;
  activityId: string;
  title: string;
  status: 'draft' | 'ready' | 'completed';
};
