export class Dependent {
  name: string;
}

export class Spouse {
  name: string;
}

export class Employee {
  name: string;
  spouse?: Spouse;
  dependents?: Dependent[];
}
