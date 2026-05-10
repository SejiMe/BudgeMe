import { PropsWithChildren } from 'react';
import { SafeAreaProvider } from 'react-native-safe-area-context';

import { QueryProvider } from './QueryProvider';
import { TamaguiRootProvider } from './TamaguiRootProvider';

export function AppProviders({ children }: PropsWithChildren) {
  return (
    <SafeAreaProvider>
      <TamaguiRootProvider>
        <QueryProvider>{children}</QueryProvider>
      </TamaguiRootProvider>
    </SafeAreaProvider>
  );
}
