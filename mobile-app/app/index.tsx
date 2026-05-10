import { CalendarPlus, ChartNoAxesColumnIncreasing, ReceiptText } from 'lucide-react-native';
import type { ReactNode } from 'react';
import { Pressable, StyleSheet, Text, View } from 'react-native';

import { AppScreen } from '@/shared/ui/AppScreen';

export default function HomeScreen() {
  return (
    <AppScreen>
      <View style={styles.content}>
        <View style={styles.header}>
          <Text style={styles.title}>BudgeMe</Text>
          <Text style={styles.subtitle}>
            Plan the spend, record what happened, and keep the next choice sharper.
          </Text>
        </View>

        <View style={styles.actions}>
          <ActionButton icon={<CalendarPlus size={22} />} label="Plan activity" />
          <ActionButton icon={<ReceiptText size={22} />} label="Record spend" />
          <ActionButton icon={<ChartNoAxesColumnIncreasing size={22} />} label="View insights" />
        </View>

        <View style={styles.emptyState}>
          <Text style={styles.emptyText}>No activity planned yet.</Text>
        </View>
      </View>
    </AppScreen>
  );
}

type ActionButtonProps = {
  icon: ReactNode;
  label: string;
};

function ActionButton({ icon, label }: ActionButtonProps) {
  return (
    <Pressable style={styles.actionButton}>
      <View style={styles.actionIcon}>{icon}</View>
      <Text style={styles.actionLabel}>{label}</Text>
    </Pressable>
  );
}

const styles = StyleSheet.create({
  actionButton: {
    alignItems: 'center',
    backgroundColor: '#ffffff',
    borderColor: '#d9e2dc',
    borderRadius: 8,
    borderWidth: 1,
    flexDirection: 'row',
    gap: 12,
    minHeight: 56,
    paddingHorizontal: 16,
  },
  actionIcon: {
    alignItems: 'center',
    height: 28,
    justifyContent: 'center',
    width: 28,
  },
  actionLabel: {
    color: '#17211b',
    fontSize: 16,
    fontWeight: '600',
  },
  actions: {
    gap: 12,
  },
  content: {
    gap: 24,
  },
  emptyState: {
    backgroundColor: '#edf4ef',
    borderRadius: 8,
    padding: 16,
  },
  emptyText: {
    color: '#4b5b51',
    fontSize: 14,
  },
  header: {
    gap: 8,
  },
  subtitle: {
    color: '#536157',
    fontSize: 17,
    lineHeight: 24,
  },
  title: {
    color: '#17211b',
    fontSize: 36,
    fontWeight: '700',
  },
});
