export class FormValuesOptions {
  private dateKeys: string[];
  private dropDownKeys: string[];

  constructor(
    dateKeys: string[] = [],
    dropDownKeys: string[] = [],
    isKeysCaseSensitive = false
  ) {
    if (isKeysCaseSensitive) {
      this.dateKeys = dateKeys;
      this.dropDownKeys = dropDownKeys;
    } else {
      this.dateKeys = [];
      this.dropDownKeys = [];

      dateKeys.forEach(dateKey => {
        this.dateKeys.push(dateKey.toLowerCase());
      });
      dropDownKeys.forEach(dropDownKey => {
        this.dropDownKeys.push(dropDownKey.toLowerCase());
      });
    }
  }

  get DateKeys(): string[] {
    return this.dateKeys;
  }

  get DropDownKeys(): string[] {
    return this.dropDownKeys;
  }
}
