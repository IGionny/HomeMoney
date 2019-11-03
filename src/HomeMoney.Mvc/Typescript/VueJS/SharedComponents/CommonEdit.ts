import Vue from 'vue';
import Component from 'vue-class-component';
import {Prop, Watch} from "vue-property-decorator";
import {GetAsync, SaveAsync} from "../Utilities/AxiosHelpers";
import {IEntity} from "../../@types/IEntity";
import {IResultModel} from "../../@types/IResultModel";

@Component({})
export default class CommonEdit<T extends IEntity> extends Vue {
  valid: boolean = true;
  entityName: string = "";
  Item !: T;

  @Prop({required: true}) Id!: string;

  beforeRouteUpdate(to: any, from: object, next: Function) {
    this.Id = to.params.Id;
    next();
  }

  LoadAccount() {
    const self = this;
    if (this.Id !== null && this.Id !== undefined) {
      //todo: threat 404
      GetAsync(this.Id, this.entityName).then((data: T) => self.Item = data);
    }
  }

  mounted() {
    this.LoadAccount();
  }

  validate() {
    if ((<any>this.$refs.form).validate()) {

    }
  }

  reset() {
    (<any>this.$refs.form).reset()
  }

  resetValidation() {
    (<any>this.$refs.form).resetValidation();
  }

  Save() {
    const self = this;
    SaveAsync<T>(this.Item, this.entityName).then((response: IResultModel<T>) => {
      self.Item = response.Value;
      //todo: Message info ok;
      self.$router.push({path: "/" + this.entityName});
    });
  }

  Cancel() {
    this.$router.push({path: "/" + this.entityName});
  }

  beforeRouteLeave(to: any, from: object, next: Function) {
    const answer = window.confirm('Do you really want to leave? you have unsaved changes!')
    if (answer) {
      next()
    } else {
      next(false)
    }
  }
}
