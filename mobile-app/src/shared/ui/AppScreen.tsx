import { PropsWithChildren } from 'react';
import { ScrollView, StyleSheet, View } from 'react-native';
import { useSafeAreaInsets } from 'react-native-safe-area-context';

export function AppScreen({ children }: PropsWithChildren) {
  const insets = useSafeAreaInsets();

  return (
    <ScrollView
      style={styles.root}
      contentContainerStyle={{
        flexGrow: 1,
        paddingBottom: Math.max(insets.bottom, 24),
        paddingLeft: 20,
        paddingRight: 20,
        paddingTop: Math.max(insets.top, 24),
      }}
    >
      <View style={styles.inner}>
        {children}
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  inner: {
    flex: 1,
    maxWidth: 720,
    width: '100%',
  },
  root: {
    backgroundColor: '#f7faf7',
  },
});
