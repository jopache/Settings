import { Component, OnInit, Input} from '@angular/core';
import { TreeNode } from '../treenode';


@Component({
  selector: 'tree-node',
  templateUrl: './tree-node.component.html',
  styleUrls: ['./tree-node.component.scss']
})
export class TreeNodeComponent implements OnInit {
  @Input() node: TreeNode;
  @Input() selectedNode:  { node: TreeNode };

  constructor() { }

  onSelect(treeNode: TreeNode): void {
    //i should probably be doing this through a service
    //and maybe using an observable?  Not sure of
    //the best approach here. 
    this.selectedNode.node = treeNode;
  }

  ngOnInit() {
  }
}
