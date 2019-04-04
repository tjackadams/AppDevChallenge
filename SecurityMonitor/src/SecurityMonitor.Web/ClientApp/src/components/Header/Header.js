import React from "react";
import { Stack } from "office-ui-fabric-react/lib/Stack";
import { Text } from "office-ui-fabric-react/lib/Text";
import {
  mergeStyleSets,
  DefaultPalette
} from "office-ui-fabric-react/lib/Styling";

const styles = mergeStyleSets({
  header: {
    color: DefaultPalette.white,
    background: DefaultPalette.themePrimary,
    padding: 20
  }
});
const Header = () => (
  <Stack.Item align="stretch" className={styles.header}>
    <Text variant="xxLarge" nowrap block>
      Security Monitor
    </Text>
  </Stack.Item>
);

export default Header;
