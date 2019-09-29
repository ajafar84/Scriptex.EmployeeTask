import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DropdownModule } from 'primeng/dropdown';
import { PaginatorModule } from 'primeng/paginator';

import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { TableModule } from 'primeng/table';
import { ContextMenuModule } from 'primeng/contextmenu';
import { InputMaskModule } from 'primeng/inputmask';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { RouterModule } from '@angular/router';
import { SharedRoutingModule } from './shared-routing.module';
import { InputSwitchModule } from 'primeng/inputswitch';
import { ToastrModule } from 'ngx-toastr';

import { LayoutComponent } from './components/layout/layout.component';
import { HiddenColumnFilterPipe } from './pipes/hidden-coulmn-filter.pipe';

@NgModule({
  declarations: [LayoutComponent, HiddenColumnFilterPipe],
  imports: [
    CommonModule,
    CommonModule,
    ProgressSpinnerModule,
    AutoCompleteModule,
    PaginatorModule,
    InputMaskModule,
    TableModule,
    AutoCompleteModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    SharedRoutingModule,
    InputSwitchModule,
    ToastrModule.forRoot({
      closeButton: true,
      positionClass: 'toast-top-center',
      progressBar: true,
      newestOnTop: true
    })
  ],
  exports: [
    LayoutComponent,
    TableModule,
    AutoCompleteModule,
    ReactiveFormsModule,
    FormsModule,
    DropdownModule,
    CommonModule,
    ProgressSpinnerModule,
    AutoCompleteModule,
    FormsModule,
    ReactiveFormsModule,
    ContextMenuModule,
    InputMaskModule,
    PaginatorModule,
    HiddenColumnFilterPipe,
    InputSwitchModule,
    ToastrModule
  ],
  providers: [HiddenColumnFilterPipe]
})
export class SharedModule {}
