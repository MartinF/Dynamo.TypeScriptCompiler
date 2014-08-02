/// <reference path="B.ts" />

module C {
	export class FooC extends B.FooB {
		public bar(): void {
			alert("FooC");
		}
	}
}