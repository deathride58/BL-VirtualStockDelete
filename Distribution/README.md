# Virtual Stock Delete

This mod does exactly as it implies on the tin: it disables SLZ's virtual stock simulation. This massively improves the overall gamefeel, especially if you're cross-eye dominant, or using avatars that're too small to properly shoulder most stocks. Rifles are borderline unusable if you're both cross-eye dominant and using a small avatar.

However, this doesn't affect the actual collision of guns, which results in some modded guns bumping into your chest as a result. Vanilla guns don't suffer from this issue in my testing.

This mod can be toggled on and off at runtime via the BoneMenu, under the "Virtual Stock Delete" section.

### BEFORE

![Before removing virtual stock](https://raw.githubusercontent.com/deathride58/BL-VirtualStockDelete/refs/heads/master/Media/virtualstockbefore.gif)

### AFTER

![After removing virtual stock](https://raw.githubusercontent.com/deathride58/BL-VirtualStockDelete/refs/heads/master/Media/virtualstockafter.gif)

## Technical details

This mod works in a fairly straight-forward manner: it overrides the `OnVirtualControllerSolve` function of `RifleVirtualController`. This function is responsible for the virtual stock simulation.

When this mod is enabled, it turns the function into a no-op. When disabled, it lets the function run. This works because virtual controller overrides operate directly on the `VirtualControlerPayload` (SLZ's typo, not mine) passed to them, so by not running the override's function to begin with, the virtual controller doesn't get affected by the stock simulation.

You can achieve an effect similar to this via UnityExplorer by either removing a given gun's `RifleVirtualController` component, or modifying its values to make the virtual stock not kick in. However, this method is destructive, and would probably give Fusion a stroke.

This mod is made and tested for Patch 6. With the sweeping changes to the player rig, this mod may potentially need updating for Patch 7.

This hasn't actually been tested with Fusion, but it should work perfectly fine in theory.