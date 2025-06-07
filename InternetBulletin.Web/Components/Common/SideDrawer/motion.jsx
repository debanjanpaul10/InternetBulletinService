import {
	createPresenceComponent,
	motionTokens,
	tokens,
} from "@fluentui/react-components";

/*
 * Create a custom DrawerMotion component that animates the drawer surface.
 */
const DrawerMotion = createPresenceComponent(() => {
	const keyframes = [
		{
			opacity: 0,
			transform: "translate3D(-100%, 0, 0)",
			margin: 0,
			backgroundColor: tokens.colorNeutralBackground1,
			borderColor: tokens.colorNeutralBackground1,
			borderRadius: 0,
		},
		{
			opacity: 1,
			transform: "translate3D(0, 0, 0)",
			margin: tokens.spacingVerticalM,
			backgroundColor: tokens.colorNeutralBackground3,
			borderColor: tokens.colorNeutralBackground4,
			borderRadius: tokens.borderRadiusXLarge,
		},
	];

	return {
		enter: {
			keyframes,
			duration: motionTokens.durationNormal,
			easing: motionTokens.curveDecelerateMin,
		},
		exit: {
			keyframes: [...keyframes].reverse(),
			duration: motionTokens.durationSlow,
			easing: motionTokens.curveAccelerateMin,
		},
	};
});

export { DrawerMotion };
