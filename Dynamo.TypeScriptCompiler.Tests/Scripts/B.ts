/// <reference path="A.ts" />

module B
{
	export class FooB implements A.IFoo {
		public bar(): void {
			alert("FooB");
		}
	}	
}