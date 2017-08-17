import { Component } from '@angular/core';
import { TreeNode  } from './treenode'

const tree: TreeNode = {
    name: "global", id: 1, children: [
      {
        name: "nested", id: 2, children: [
          {
            name: "nested-console", id: 3, children: [
              { name: "nested-console-again", id: 3, children: []}
            ]
          }
        ]
      },
      {name: "nested-web app", id: 3, children: []}
    ]
  };
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';
  node = tree;
}
