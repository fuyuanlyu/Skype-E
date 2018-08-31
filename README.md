# Skype-E

## Summary

One textual sentence might have several different interpretations, and this ambiguity of human language can easily lead to misunderstanding in online textual communication. In this demo, we designed and implemented Skype-E, an emotion-based plugin for telecommunication software like Skype. With a laptop camera, Skype-E detects and recognizes the facial expression changes of a user, and notifies the other user by rerendering their graphical user interfaces according to the detected emotion. This plugin is activated only under the agreement of both users. The raw images captured by the camera are kept secret from other users. 

## Demonstration



## Prerequisite

The following packages are necessary for running this demo: 

* Microsoft.ProjectOxford.Emotion.1.0.0.1
* Newtonsoft.Json.9.0.1
* AForge.2.2.5
* AForge.Controls.2.2.5
* AForge.Imaging.2.2.5
* AForge.Math.2.2.5
* AForge.Video.2.2.5
* AForge.Video.DirectShow.2.2.5

## Preprocessing

We preprocess the images to invert the artificial disturbance as follows:

```python
cd traditional-learning-method/
python3 preprocessing.py
```

![](./Presentation/preprocessing.png)

## Traditional Methods

* Enter the [traditional-learning-method/](./traditional-learning-method) folder:

  `cd traditional-learning-method/`

* KNN:

  `python3 KNN.py`

* SVM:

  `python3 SVM.py`

* Ensemble method:

  `python3 voting.py`

## CNN

We try three kinds of network architectures, SimpleCNN, VGG and Resnet.

Usage:

```
	cd deep-learning-method/
	python tools/train.py [--gpu GPU-ID] [--dataset DATASET] [--net NET] 
	[--resume path/to/resume/from] [--logdir path/to/log/dir]

	arguments:
	--gpu GPU-ID    		the id of GPU card to use
	--dataset DATASET  		the dataset to train and test
	--net NET     			the network structure to use
					options: ConvNet,vgg16,vgg11,res18,res34,res50,res101      
	--resume path/to/resume/from	resume training from the given path
	--logdir path/to/log/dir	directory to log 
```

## Capsule

Usage:

```
	cd capsule/
	CUDA_VISIBLE_DEVICES=0 python3 capsule_network.py
```

## Results

|  Model   | Accuracy / % |         Model          | Accuracy / % |
| :------: | :----------: | :--------------------: | :----------: |
|   KNN    |    98.40     | KNN without preprocess |    88.12     |
|   SVM    |    98.75     | SVM without preprocess |    93.22     |
| Ensemble |    98.40     |                        |              |
|  VGG16 |       99.79  |  VGG16 without preprocess |    99.71     |
| Res101     |  **99.83**  |  Res101 without preprocess |    99.73    |
|  CapsNet      | 99.80  | CapsNet without preprocess |    99.78   |



## Team Member

* [Weichao Mao](https://github.com/xizeroplus)
* [Ruiheng Chang](https://github.com/crh19970307)



