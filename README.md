# News-Ninja
This repository includes all files relevant to the paper "News Ninja: Gamified Annotation Of Linguistic Bias In Online News"

## About
Recent research shows that visualizing linguistic bias mitigates its negative effects. However, reliable automatic detection methods to generate such visualizations require costly, knowledge-intensive training data. To facilitate data collection for media bias datasets, we present News Ninja, a game employing data-collecting game mechanics to generate a crowdsourced dataset. Before annotating sentences, players are educated on media bias via a tutorial. Our findings show that datasets gathered with crowdsourced workers trained on News Ninja can reach significantly higher inter-annotator agreements than expert and crowdsourced datasets with similar data quality. As News Ninja encourages continuous play, it allows datasets to adapt to the reception and contextualization of news over time, presenting a promising strategy to reduce data collection expenses, educate players, and promote long-term bias mitigation.

## Content
News Ninja Source Code
News Ninja Dataset
Video of News Ninja 

## News Ninja Dataset
This dataset was created through player annotations in the News Ninja Game made by ANON. Its goal is to improve the detection of linguistic media bias. Support came from ANON. None of the funders played any role in the dataset creation process or publication-related decisions.

The dataset includes sentences with binary bias labels (processed, biased or not biased) as well as the annotations of single players used for the majority vote. It includes all game-collected data. All data is completely anonymous. The dataset does not identify sub-populations or can be considered sensitive to them, nor is it possible to identify individuals.

Some sentences might be offensive or triggering as they were taken from biased or more extreme news sources. The dataset contains topics such as violence, abortion, and hate against specific races, genders, religions, or sexual orientations.

### Description of the Data Files
This repository contains the datasets for the anonymous News Ninja submission. The tables contain the following data:

**ExportNewsNinja.csv:** Contains 370 BABE sentences and 150 new sentences with their text (sentence), words labeled as biased (words), BABE ground truth (ground_Truth), and the sentence bias label from the player annotations (majority_vote). The first 370 sentences are re-annotated BABE sentences, and the following 150 sentences are new sentences.

**AnalysisNewsNinja.xlsx:** Contains 370 BABE sentences and 150 new sentences. The first 370 sentences are re-annotated BABE sentences, and the following 150 sentences are new sentences. The table includes the full sentence (Sentence), the sentence bias label from player annotations (isBiased Game), the new expert label (isBiased Expert), if the game label and expert label match (Game VS Expert), if differing labels are a false positives or false negatives (false negative, false positive), the ground truth label from BABE (isBiasedBABE), if Expert and BABE labels match (Expert VS BABE), and if the game label and BABE label match (Game VS BABE). It also includes the analysis of the agreement between the three rater categories (Game, Expert, BABE).

**demographics.csv:** Contains demographic information of News Ninja players, including gender, age, education, English proficiency, political orientation, news consumption, and consumed outlets.

 
### Collection Process
Data was collected through interactions with the NewsNinja game. All participants went through a tutorial before annotating 2x10 BABE sentences and 2x10 new sentences. For this first test, players were recruited using Prolific. The game was hosted on a costume-built responsive website. The collection period was from 20.02.2023 to 28.02.2023. Before starting the game, players were informed about the goal and the data processing. After consenting, they could proceed to the tutorial.

The dataset will be open source. A link with all details and contact information will be provided upon acceptance. No third parties are involved.

The dataset will not be maintained as it captures the first test of NewsNinja at a specific point in time. However, new datasets will arise from further iterations. Those will be linked in this repository. Please cite the NewsNinja paper if you use the dataset and contact us if you're interested in more information or joining the project.
