using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary{
	public class BuilderAntFactory : AntFactory<BuilderAntData>{
		// Start is called before the first frame update
		void Start(){
        
		}

		// Update is called once per frame
		void Update(){
        
		}

		public override Ant InstantiateAnt(BuilderAntData antData){
			throw new System.NotImplementedException();
		}
	}

}
