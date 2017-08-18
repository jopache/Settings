import { Component, OnInit, Input} from '@angular/core';
import { TreeNode } from '../treenode';


@Component({
  selector: 'tree-node',
  templateUrl: './tree-node.component.html',
  styleUrls: ['./tree-node.component.scss']
})
export class TreeNodeComponent implements OnInit {
  @Input() node: TreeNode;
    constructor() { }

  onSelect(treeNode: TreeNode): void {
    console.log(treeNode);
  }

  ngOnInit() {

  }

}
