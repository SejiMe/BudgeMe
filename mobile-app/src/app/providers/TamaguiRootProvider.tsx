import { PropsWithChildren } from 'react';
import { TamaguiProvider } from 'tamagui';

import { tamaguiConfig } from '@/theme/tamagui.config';

export function TamaguiRootProvider({ children }: PropsWithChildren) {
  return (
    <TamaguiProvider config={tamaguiConfig} defaultTheme="light">
      {children}
    </TamaguiProvider>
  );
}
