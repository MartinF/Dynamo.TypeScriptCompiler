module A
{
	export interface IFoo {
		bar() : void;
	}

	export class Foo implements IFoo
	{
		public bar(): void {
			alert("Foo");
		}
	}
}