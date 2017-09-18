import { TreeNodeSelector } from '../services/treeNodeSelector';
import { Component, OnInit, Input} from '@angular/core';
import { TreeNode } from '../treenode';


@Component({
  selector: 'app-tree-node',
  templateUrl: './tree-node.component.html',
  styleUrls: ['./tree-node.component.scss']
})
export class TreeNodeComponent implements OnInit {
  @Input() node: TreeNode;
  @Input() treeNodeSelector: TreeNodeSelector;
  active = false;


  constructor() { }

  onSelect(treeNode: TreeNode): void {
    this.treeNodeSelector.setActiveNode(treeNode);
  }

  ngOnInit() {
    this.treeNodeSelector.activeNode.subscribe(treeNode => {
    if (this.node.id === treeNode.id) {
      console.log('setting active true. component node', this.node.name, 'selected node', treeNode.name);
      this.active = true;
    } else {
      this.active = false;
    }
    });
  }
}
